#!/bin/bash

sudo yum update -y 

echo "Installing AWS CodeDeploy Agent..."
sudo yum install ruby -y 

wget https://aws-codedeploy-us-east-1.s3.us-east-1.amazonaws.com/latest/install
chmod +x ./install
sudo ./install auto
sudo systemctl start codedeploy-agent
sudo systemctl enable codedeploy-agent

echo "Installing Nginx..."
sudo amazon-linux-extras enable nginx1
sudo yum install nginx -y  

sudo systemctl start nginx
sudo systemctl enable nginx

echo "Installing Node.js..."
curl -fsSL https://rpm.nodesource.com/setup_22.x | sudo bash -
sudo yum install -y nsolid
sudo yum install nodejs22

echo "Installing Angular CLI..."
sudo npm install -g @angular/cli

echo "Creating deployment directory..."
sudo mkdir -p /var/www/aws-chat
sudo chown -R ec2-user:ec2-user /var/www/aws-chat

echo "Configuring Nginx..."
sudo tee /etc/nginx/nginx.conf > /dev/null <<EOL
events{}

http {
    include /etc/nginx/mime.types;

    server {
        listen 80;
        listen [::] 80;
        root /var/www/aws-chat;
        index index.html;

        location / {
            try_files $uri $uri/ /index.html;
        }
    }
}
EOL

sudo systemctl restart nginx

echo "Setup complete"
