# docker-web-app
Example of using ASP.NET Core and MSSQL Server inside docker container.

## Build docker image
```bash
docker build -t maxrok98/docker-webappdb-example .
```

## Run docker container
```bash
docker run -t --name web-api -p 5000:80 -i maxrok98/docker-webappdb-example
```


## Minikube commands
```bash
minikube start

kubectl apply -f .kube/deployment.yaml
kubectl apply -f .kube/service.yaml
kubectl get services # check port
kubectl get pods
kubectl get nodes

kubectl describe pod <pod-name>
kubectl logs <pod-name>

kubectl get node -o wide # check external IP
kubectl get svc
minikube service web-app --url

kubectl delete deployment web-app
kubectl delete service web-app
kubectl delete service kubernetes

minikube stop
minikube delete
```

