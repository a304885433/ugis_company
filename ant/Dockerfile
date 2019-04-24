FROM exiasr/alpine-yarn-nginx:10.14.2
WORKDIR /app
COPY . /app/
EXPOSE 80
RUN  yarn install \ 
    && yarn build  \
    && cp -r dist/* /usr/share/nginx/html \
    && cp -r nginx/* /etc/nginx/conf.d/ \
    && rm -rf /app
CMD ["nginx","-g","daemon off;"]