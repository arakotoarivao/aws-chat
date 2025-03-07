#!/bin/bash
sudo systemctl stop nginx

folder="/var/www/aws-chat/"
ongoing_deployment=$(ls -t /opt/codedeploy-agent/deployment-root/ | head -n 1)

cd /opt/codedeploy-agent/deployment-root/$ongoing_deployment/ || exit 1

latest_deployment=$(ls -t $pwd | head -n 1)

file $latest_deployment
sudo unzip -l $latest_deployment
sudo unzip -l $latest_deployment/artifact.zip
cd $latest_deployment || exit 1

ls -l

if [ -f artifact.zip ]; then
  echo "Unzipping artifact.zip..."
  sudo unzip -o artifact.zip -d /opt/codedeploy-agent/deployment-root/$ongoing_deployment/ || exit 1
else
  echo "artifact.zip not found!"
  exit 1
fi

echo "Deploying files..."
sudo cp -r /opt/codedeploy-agent/deployment-root/$ongoing_deployment/app/dist/app/* $folder

chown -R nginx:nginx $folder
chmod -R 755 $folder