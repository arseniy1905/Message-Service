import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, timer } from 'rxjs';
@Component({
  selector: 'plant-list-data',
  templateUrl: './plant-list.component.html',
  standalone:false
})

export class PlantDataComponent implements OnInit, OnDestroy {
  subscription!: Subscription;
  everyFiveSeconds: Observable<number> = timer(0, 5000);
  public plants!: Plant[];
  //baseUrl: string;
  http: HttpClient;
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  this.http = http;
  //  this.baseUrl = baseUrl;
  //  this.getComponents();
  //}
  constructor(http: HttpClient) {
    this.http = http;
    //this.baseUrl = baseUrl;
    this.getComponents();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  ngOnInit() {
    this.subscription = this.everyFiveSeconds.subscribe(() => {
      this.getComponents();
    });
  }
  //getComponents() {
  //  this.http.get<Plant[]>(this.baseUrl + 'plants').subscribe(result => {
  //    this.plants = result;
  //  }, error => console.error(error));
  //}
  getComponents() {
    //let headers = new HttpHeaders();
    //headers.append('Access-Control-Allow-Origin', '*');
    //headers.append('Content-Type', 'application/json');
    this.http.get<Plant[]>('/plants').subscribe(result => {
      this.plants = result;
    }, error => console.error(error));
  }
  waterMe(id: number) {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    headers.append('id', id.toString());
    this.http.get<Plant[]>('/plants/' + id.toString()).subscribe(result => {
      this.plants = result;
    }, error => console.error(error));
  }

}
interface Plant {
  id: number;
  name: string;
  image: string;
  isWatered: boolean;
  isWatering: boolean;
  cannotWater: boolean;
}
