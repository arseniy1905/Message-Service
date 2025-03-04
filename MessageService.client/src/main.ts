import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { enableProdMode, inject } from '@angular/core';
import { environment } from './environments/environment';
export function getBaseUrl() {
  //var currentUrl = document.getElementsByTagName('base')[0].href;
  //const regex = /^(https?:\/\/[^\/:]+)/;
  //const match = currentUrl.match(regex);
  var baseUrl = "https://localhost:7072";
  //var currentUrl = document.getElementsByTagName('base')[0].href;
  //var baseUrl = "https://localhost:56329/";
   
  //var baseUrl = document.getElementsByTagName('base')[0].href;
  return baseUrl;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}
platformBrowserDynamic(providers).bootstrapModule(AppModule, {
  ngZoneEventCoalescing: true,
  
})
  .catch(err => console.error(err));
