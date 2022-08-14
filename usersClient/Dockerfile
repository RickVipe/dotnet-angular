FROM node:18.7.0-alpine3.15 as builder
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build --prod

FROM nginx:1.23.1
EXPOSE 80
COPY --from=builder /app/dist/users-client /usr/share/nginx/html
