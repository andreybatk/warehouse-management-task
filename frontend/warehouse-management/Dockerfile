FROM node:20-alpine as build

WORKDIR /app
COPY . .

RUN npm install
RUN npm run build --prod

FROM nginx:alpine

COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/warehouse-management/browser /usr/share/nginx/html