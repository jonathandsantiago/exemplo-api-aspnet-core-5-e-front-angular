FROM node:10.16.3 as angular
WORKDIR /app
COPY package.json /app
RUN npm install --silent
COPY . .
RUN npm run build -- --aot=true --build-optimizer=true --optimization=true --prod --configuration=production --base-href=/ --output-path=/app/www

FROM nginx:alpine
VOLUME /var/cache/nginx
COPY --from=angular app/dist/ewave-gestor /usr/share/nginx/html
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf

