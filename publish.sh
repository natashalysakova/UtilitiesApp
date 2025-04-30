#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

tag_and_push(){

    src="$2$1:latest"

    trg2="$2$1:$3"
    tg3="$2$1:latest-arm64v8"
    tg4="$2$1:$3-arm64v8"

    docker tag $src $trg2
    docker tag $src $tg3
    docker tag $src $tg4

    docker image push $trg2
    docker image push $tg3
    docker image push $tg4
}

read -p 'Version: ' VERSION

source .env

docker compose build 

IMAGE='utilityapp-migrationservice'
tag_and_push "$IMAGE" "$REGISTRY_URL" "$VERSION"

IMAGE='utilityapp-apiservice'
tag_and_push "$IMAGE" "$REGISTRY_URL" "$VERSION"

IMAGE='utilityapp-webfrontend'
tag_and_push "$IMAGE" "$REGISTRY_URL" "$VERSION"