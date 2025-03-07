#!/bin/bash
sudo systemctl start nginx

cd /var/www/aws-chat

if [ -f artifact.zip ]; then
    echo "Unzipping artifact.zip..."
  
    sudo unzip -o artifact.zip || exit 1
    sudo rm artifact.zip

    echo "Unzipping finished..."
else
  echo "artifact.zip not found!"
  exit 1
fi