variable "mysql_user" {
  description = "MySQL user"
  sensitive = true
}

variable "mysql_password" {
  description = "MySQL password"
  sensitive = true
}

variable "lab_account_id" {
  description = "AWS Labs account ID"
  sensitive = true
}

variable "lab_account_region" {
  description = "AWS Labs account region"
  default = "us-west-2"
}

variable "work_tracker_image" {
  description = "Work Tracker Docker image"
}