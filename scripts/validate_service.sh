#!/bin/bash
if curl -I http://localhost | grep "200 OK"; then
  echo "Validation successful: The app is running."
  exit 0
else
  echo "Validation failed: The app is not running."
  exit 1
fi