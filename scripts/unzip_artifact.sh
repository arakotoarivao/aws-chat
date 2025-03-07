#!/bin/bash
ls-l
unzip -o artifact.zip -d /var/www/aws-chat

chown -R nginx:nginx /var/www/aws-chat
chmod -R 755 /var/www/aws-chat

sudo systemctl stop nginx