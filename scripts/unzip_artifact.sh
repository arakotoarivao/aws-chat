#!/bin/bash
sudo systemctl stop nginx

folder="/var/www/aws-chat/"
ls -l
ls -l $folder

latest_deployment=$(ls -t /opt/codedeploy-agent/deployment-root/ | head -n 1)

cd /opt/codedeploy-agent/deployment-root/$latest_deployment/ || exit 1

ls -l

if [ -f artifact.zip ]; then
  echo "Unzipping artifact.zip..."
  sudo unzip -o artifact.zip -d /opt/codedeploy-agent/deployment-root/$latest_deployment/ || exit 1
else
  echo "artifact.zip not found!"
  exit 1
fi

echo "Deploying files..."
sudo cp -r /opt/codedeploy-agent/deployment-root/$latest_deployment/app/dist/app/* $folder

chown -R nginx:nginx $folder
chmod -R 755 $folder