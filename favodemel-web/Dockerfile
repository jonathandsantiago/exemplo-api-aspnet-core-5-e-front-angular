FROM node:10.16.3 as angular
WORKDIR /app
COPY package.json /app
RUN npm install --silent --force
COPY . .
RUN npm run build --prod --aot --build-optimizer --common-chunk --vendor-chunk --optimization --progress

FROM nginx:alpine
VOLUME /var/cache/nginx
COPY --from=angular app/dist/favodemel-web /usr/share/nginx/html
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf
