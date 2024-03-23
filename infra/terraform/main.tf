terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
    }
  }
  required_version = ">= 1.3.7"
  backend "s3" {
    bucket                  = "terraform-buckets-work-tracker"
    key                     = "work-tracker/terraform.tfstate"
    region                  = var.lab_account_region
  }
}
provider "aws" {
    region = var.lab_account_region
}

data "aws_security_group" "default" {
  name = "default"
}

data "aws_vpc" "default" {
  default = true
}

data "aws_subnets" "default" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.default.id]
  }
}

locals {
  endpoint_parts = split(":", aws_db_instance.work-tracker-mysql.endpoint)
  port = local.endpoint_parts[1]
}

resource "aws_db_instance" "work-tracker-mysql" {
  engine               = "mysql"
  identifier           = "work-tracker-db"
  allocated_storage    =  20
  engine_version       = "8.0.33"
  instance_class       = "db.t2.micro"
  username             = var.mysql_user
  password             = var.mysql_password
  parameter_group_name = "work-tracker-mysql-parameter"
  vpc_security_group_ids = [data.aws_security_group.default.id]
  skip_final_snapshot  = true
  publicly_accessible =  true
}

resource "aws_ecs_task_definition" "work-tracker-task" {
  depends_on = [ aws_db_instance.work-tracker-mysql ]
  family                   = "work-tracker"
  network_mode             = "awsvpc"
  execution_role_arn       = "arn:aws:iam::${var.lab_account_id}:role/LabRole"
  cpu                      = 256
  memory                   = 512
  requires_compatibilities = ["FARGATE"]
  container_definitions    = jsonencode([
    {
        "name": "work-tracker",
        "image": var.work_tracker_image,
        "essential": true,
        "portMappings": [
            {
              "containerPort": 80,
              "hostPort": 80,
              "protocol": "tcp"
            }
        ],
        "environment": [
            {
                "name": "ConnectionStrings__DefaultConnection",
                "value": join("", ["Server=", element(split(":", aws_db_instance.work-tracker-mysql.endpoint), 0), ";Port=", local.port, ";Database=worktracker;Uid=", var.mysql_user, ";Pwd=", var.mysql_password, ";"])
            },
            {
                "name": "Mailtrap__Host",
                "value": var.mailtrap_host
            },
            {
                "name": "Mailtrap__Username",
                "value": var.mailtrap_username
            },
            {
                "name": "Mailtrap__Password",
                "value": var.mailtrap_password
            },
            {
                "name": "Cognito__Authority",
                "value": var.cognito_authority
            },
            {
                "name": "Cognito__ClientId",
                "value": var.cognito_client_id
            },
            {
                "name": "Cognito__IdpUrl",
                "value": var.cognito_idp_url
            }
        ],
        "cpu": 256,
        "memory": 512,
        "logConfiguration": {
            "logDriver": "awslogs",
            "options": {
                "awslogs-group": "work-tracker-logs",
                "awslogs-region": "us-west-2",
                "awslogs-stream-prefix": "work-tracker"
            }
        }
    }
  ])
}

resource "aws_lb_target_group" "work-tracker-api-tg" {
  name     = "work-tracker-api"
  port     = 80
  protocol = "HTTP"
  vpc_id   = data.aws_vpc.default.id
  target_type = "ip"

  health_check {
    enabled = true
    interval = 300
    path = "/health-check"
    protocol = "HTTP"
    timeout = 60
    healthy_threshold = 3
    unhealthy_threshold = 3
  }
}

resource "aws_lb" "work-tracker-api-lb" {
  name               = "work-tracker-api"
  internal           = true
  load_balancer_type = "application"
  subnets            = data.aws_subnets.default.ids
}

resource "aws_lb_listener" "work-tracker-api-lbl" {
  load_balancer_arn = aws_lb.work-tracker-api-lb.arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.work-tracker-api-tg.arn
  }
}

resource "aws_ecs_service" "work-tracker-service" {
  name            = "work-tracker-service"
  cluster         = "food-totem-ecs"
  task_definition = aws_ecs_task_definition.work-tracker-task.arn
  desired_count   = 1
  launch_type     = "FARGATE"

  network_configuration {
    security_groups  = [data.aws_security_group.default.id]
    subnets = data.aws_subnets.default.ids
    assign_public_ip = true
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.work-tracker-api-tg.arn
    container_name   = "work-tracker"
    container_port   = 80
  }

  health_check_grace_period_seconds = 120
}

resource "aws_cloudwatch_log_group" "work-tracker-logs" {
  name = "work-tracker-logs"
  retention_in_days = 1
}