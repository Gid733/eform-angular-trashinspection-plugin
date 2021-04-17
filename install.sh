#!/bin/bash

if [ ! -d "/var/www/microting/eform-angular-trashinspection-plugin" ]; then
  cd /var/www/microting
  su ubuntu -c \
  "git clone https://github.com/microting/eform-angular-trashinspection-plugin.git -b stable"
fi

cd /var/www/microting/eform-angular-trashinspection-plugin
su ubuntu -c \
"dotnet restore eFormAPI/Plugins/TrashInspection.Pn/TrashInspection.Pn.sln"

echo "################## START GITVERSION ##################"
export GITVERSION=`git describe --abbrev=0 --tags | cut -d "v" -f 2`
echo $GITVERSION
echo "################## END GITVERSION ##################"
su ubuntu -c \
"dotnet publish eFormAPI/Plugins/TrashInspection.Pn/TrashInspection.Pn.sln -o out /p:Version=$GITVERSION --runtime linux-x64 --configuration Release"

su ubuntu -c \
"rm -fR /var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/trash-inspection-pn"


su ubuntu -c \
"cp -av /var/www/microting/eform-angular-trashinspection-plugin/eform-client/src/app/plugins/modules/trash-inspection-pn /var/www/microting/eform-angular-frontend/eform-client/src/app/plugins/modules/trash-inspection-pn"
su ubuntu -c \
"mkdir -p /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/"

su ubuntu -c \
"rm -fR /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/TrashInspection"

su ubuntu -c \
"cp -av /var/www/microting/eform-angular-trashinspection-plugin/out /var/www/microting/eform-angular-frontend/eFormAPI/eFormAPI.Web/out/Plugins/TrashInspection"


echo "Recompile angular"
cd /var/www/microting/eform-angular-frontend/eform-client
su ubuntu -c \
"/var/www/microting/eform-angular-trashinspection-plugin/testinginstallpn.sh"
su ubuntu -c \
"export NODE_OPTIONS=--max_old_space_size=8192 && GENERATE_SOURCEMAP=false npm run build"
echo "Recompiling angular done"
/rabbitmqadmin declare queue name=eform-angular-trashinspection-plugin durable=true

