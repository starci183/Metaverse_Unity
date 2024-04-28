FROM nginx:alpine

COPY ./Build /usr/share/nginx/html

## docker build -t scity:v1 .
## docker run -d --name scity -p 8080:80 scity:v1 