WHT='\033[1;37m' 
LGRN='\033[1;32m'
NC='\033[0m'
projects=(Futbal.Mng.Domain.Tests Futbal.Mng.Infrastructure.Tests)
for project in ${projects[*]}
do
 echo -e ${WHT}Running tests for:${LGRN} $project ${NC}
 dotnet test $project/$project.csproj
done