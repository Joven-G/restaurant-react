FROM openjdk:8-jdk-alpine

RUN apk add --no-cache curl git jq python py-pip
RUN pip install awscli
RUN apk del git jq py-pip