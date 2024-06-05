import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { Category } from '../interfaces/category';
import { CategoryCreateRequest } from '../interfaces/category-create-request';
import { CategoryCreateResponse } from '../interfaces/category-create-response';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http : HttpClient) { }

  getAllCategories() : Observable<Category[]> 
  {
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/v2/categories`)
  }

  createCategory(categoryToCreate : CategoryCreateRequest)
  {
    return this.http.post<CategoryCreateResponse>(`${environment}/api/v2/categories`,categoryToCreate)
  }

}