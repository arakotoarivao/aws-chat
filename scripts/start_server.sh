#!/bin/bash
sudo systemctl start nginx

if [ -f artifact.zip ]; then
    echo "Unzipping artifact.zip..."
  
    sudo unzip -o artifact.zip -d /var/www/aws-chat || exit 1
    sudo rm artifact.zip

    echo "Unzipping finished..."
else
  echo "artifact.zip not found!"
  exit 1
fi