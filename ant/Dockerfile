FROM exiasr/alpine-yarn-nginx:10.14.2
WORKDIR /app
COPY . /app/
EXPOSE 80
RUN  yarn install \ 
    && yarn build  \
    && cp -r dist/* /usr/share/nginx/html \
    && rm -rf /app
#&& cp -r nginx/* /etc/nginx/conf.d/ \
CMD ["nginx","-g","daemon off;"]