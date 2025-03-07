#!/bin/bash
if curl -s http://localhost | grep "AWS chat"; then
  echo "Validation successful: The app is running."
  exit 0
else
  echo "Validation failed: The app is not running."
  exit 1
fi