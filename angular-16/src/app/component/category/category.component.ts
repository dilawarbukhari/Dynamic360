import { Component, ElementRef, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { CategoryRequest } from 'src/app/Interface/Category';
import { CategoryService } from 'src/app/Services/category/category.service';
import { CommonService } from 'src/app/utilities/common.service';
declare var $: any;
@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent {
  @ViewChild('nameInputfield') nameInputfield!: ElementRef;
  isModelCheck =true;
  searchText='';
  CategoryId=0;
  CategoryResponse:any[]=[];
  filteredCategoryList: any[] = [];
   inputText=''
   currentPage: number = 1;
   itemsPerPage: number = 7;
   categoryRequest:CategoryRequest;
  destroy$: Subject<void> = new Subject<void>();


  constructor( private _service :CategoryService,private _commonService :CommonService){
 this.categoryRequest=new CategoryRequest();
  }
  ngAfterViewInit() {
    $('#categoryModal').on('shown.bs.modal', () => {
      this.nameInputfield.nativeElement.focus();
    });
  }

  ngOnInit(){
    this.GetCategory();
    this.filteredCategoryList = [...this.CategoryResponse];
  }
  openCategoryModal() {
    this.inputText=''
    this.isModelCheck = true;
    $('#categoryModal').modal('show');
  }
  
GetCategory(){
this._service.GetAllCategory().subscribe((response )=>{
this.CategoryResponse=response.data;
this.filteredCategoryList=response.data;
  })
}

  IsUpdateModel(response:any) {
    this.isModelCheck = false;
    $('#categoryModal').modal('show');
    this.inputText=response.name;
    this.categoryRequest.CategoryId=response.categoryId
  }

 

  filterCategories() {
    if (!this.searchText || this.searchText.trim() === '') {
      this.filteredCategoryList = [...this.CategoryResponse];
    } else {
      this.filteredCategoryList = this.CategoryResponse.filter(category =>
        category.name.toLowerCase().includes(this.searchText.toLowerCase())
      );
    }
    this.currentPage = 1; 
  }

  isUpdateCategory(){
    if (!this.inputText || this.inputText.length > 3){
    this.categoryRequest.Name=this.inputText;
    this._service.UpdateCategory(this.categoryRequest).subscribe((response)=>{
      this._commonService.showToast(response.message,'','success')
      $('#categoryModal').modal('hide');
      this.GetCategory();
    })
  }
  }
isSaveCategory()
{
if (!this.inputText || this.inputText.length > 3)
{
  this.categoryRequest.Name=this.inputText;
  this._service.AddCategory(this.categoryRequest).subscribe((response)=>{
    this._commonService.showToast(response.message,'','success')
    $('#categoryModal').modal('hide');
    this.GetCategory();
})
}
}

openDeleteModal(response:any){
  $('#deleteModal').modal('show');
  this.inputText=response.name
  this.CategoryId=response.categoryId;
}
deleteCategory(){
  this._service.DeleteCategory(this.CategoryId).subscribe((response)=>{
    this._commonService.showToast(response.message,'','success')
    $('#deleteModal').modal('hide');
    this.GetCategory();
});
}
}
