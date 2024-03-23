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

variable "mailtrap_host" {
  description = "Mailtrap host"
  sensitive = true
}

variable "mailtrap_username" {
  description = "Mailtrap username"
  sensitive = true
}

variable "mailtrap_password" {
  description = "Mailtrap password"
  sensitive = true
}

variable "cognito_authority" {
  description = "Cognito authority"
  sensitive = true
}

variable "cognito_client_id" {
  description = "Cognito client ID"
  sensitive = true
}

variable "cognito_idp_url" {
  description = "Cognito Idp Url"
  sensitive = true
}

variable "aws_access_key_id" {
  description = "AWS access key ID"
  sensitive = true
}

variable "aws_secret_access_key" {
  description = "AWS secret access key"
  sensitive = true
}

variable "aws_session_token" {
  description = "AWS session token"
  sensitive = true
}