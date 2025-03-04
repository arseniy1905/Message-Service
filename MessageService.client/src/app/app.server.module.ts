import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
/*import { ModuleMapLoaderModule } from '@angular/cli'*/
import { AppComponent } from './app.component';
import { AppModule } from './app.module';

@NgModule({
    imports: [AppModule, ServerModule],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
