#!/bin/bash

#export JAVA_HOME=/opt/thirdparty/jdk7
#export MAVEN_HOME=/opt/thirdparty/maven
#export PATH=$PATH:$MAVEN_HOME/bin
#export TEST_HOME=/opt/lux/LUXPromotionsTest
#export SAUCE_HOME=/opt/thirdparty/sauce-connect
#export SAUCE_USER="amelemolaligne12"
#export SAUCE_KEY="12ad92a0-2d01-43a4-945e-01cc3f8cc134"

#sudo $JAVA_HOME/bin/java -jar $SAUCE_HOME/Sauce-Connect.jar $SAUCE_USER $SAUCE_KEY

#cd $TEST_HOME
#sudo rm -rf target
#mvn clean install
mvn -Dtest=DVTTests test -Denv=qa7 -Dbrowser=cr -DskipTests=false