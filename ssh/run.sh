# stop and remove all containers
currcontainer=$(docker ps -a |  grep "$(proj.front)_$(Build.SourceBranchName)*"  | awk '{print $1}')
if [ "${currcontainer}" != "" ]; then
   docker stop $(docker ps -a |  grep "$(proj.front)_$(Build.SourceBranchName)*"  | awk '{print $1}')
   docker rm -f $(docker ps -a |  grep "$(proj.front)_$(Build.SourceBranchName)*"  | awk '{print $1}')
fi
# run curr
docker run -d --volume /home/docker_data:/var/userdata -p $(site.port):80 --name=$(proj.front)_$(Build.SourceBranchName) $(docker.repository):$(proj.front)_$(Build.BuildId)