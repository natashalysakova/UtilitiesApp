#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

tag_and_push(){

    src="$1:latest"
    trg="$2$1:latest"
    trg2="$2$1:$3"

    docker tag $src $trg
    docker tag $src $trg2

    docker image push $trg
    docker image push $trg2
}

read -p 'Version: ' VERSION

REGISTRY_URL='ghcr.io/natashalysakova/'

docker compose build

IMAGE='utilityapp-migrationservice'
tag_and_push "$IMAGE" "$REGISTRY_URL" "$VERSION"

IMAGE='utilityapp-apiservice'
tag_and_push "$IMAGE" "$REGISTRY_URL" "$VERSION"

IMAGE='utilityapp-webfrontend'
tag_and_push "$IMAGE" "$REGISTRY_URL" "$VERSION"