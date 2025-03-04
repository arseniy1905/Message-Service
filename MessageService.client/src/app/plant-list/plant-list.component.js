"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.PlantDataComponent = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var rxjs_1 = require("rxjs");
var PlantDataComponent = /** @class */ (function () {
    function PlantDataComponent(http, baseUrl) {
        this.everyFiveSeconds = (0, rxjs_1.timer)(0, 5000);
        this.http = http;
        this.baseUrl = baseUrl;
        this.getComponents();
    }
    PlantDataComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
    };
    PlantDataComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.everyFiveSeconds.subscribe(function () {
            _this.getComponents();
        });
    };
    PlantDataComponent.prototype.getComponents = function () {
        var _this = this;
        this.http.get(this.baseUrl + 'plants').subscribe(function (result) {
            _this.plants = result;
        }, function (error) { return console.error(error); });
    };
    PlantDataComponent.prototype.waterMe = function (id) {
        var _this = this;
        var headers = new http_1.HttpHeaders();
        headers.append('Content-Type', 'application/json');
        headers.append('id', id);
        this.http.get(this.baseUrl + 'plants/' + id).subscribe(function (result) {
            _this.plants = result;
        }, function (error) { return console.error(error); });
    };
    PlantDataComponent = __decorate([
        (0, core_1.Component)({
            selector: 'plant-list-data',
            templateUrl: './plant-list.component.html'
        }),
        __param(1, (0, core_1.Inject)('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], PlantDataComponent);
    return PlantDataComponent;
}());
exports.PlantDataComponent = PlantDataComponent;
//# sourceMappingURL=plant-list.component.js.map