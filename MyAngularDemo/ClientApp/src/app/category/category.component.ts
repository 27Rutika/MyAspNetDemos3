import { Component, Inject } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Category } from './category.interface';


@Component({
    selector: 'category',                           // Router Path
    templateUrl: './category.component.html',       // the UI Template
    styleUrls: ['./category.component.css']         // Component Level Custom CSS for the UI Template
})


export class CategoryComponent {

    public categories: Category[];

    // 'BASE_URL' provider is defined in the main.ts
    constructor(http: HttpClient,  @Inject('BASE_URL') baseUrl: string) {

        http.get<Category[]>(baseUrl + 'api/Categories')
            .subscribe(result => {
                console.log(result);                // for debugging purposes
                this.categories = result;
            }, error => console.error(error));
    }

}