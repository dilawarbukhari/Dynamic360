import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SupplierRequest } from 'src/app/Interface/Supplier';
import { SupplierService } from 'src/app/Services/supplier/supplier.service';
import { CommonService } from 'src/app/utilities/common.service';
declare var $: any;
@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})
export class SupplierComponent {
@ViewChild('nameInputfield') nameInputfield!: ElementRef;
  supplierFormGroup!:FormGroup;
  supplierId=0;
  searchText='';
  deleteText='';
  currentPage=1;
  itemsPerPage=5;
  SupplierResponse:any[]=[];
  filteredSupplierList: any[] = [];
  isModelCheck=true;
  supplierRequest:SupplierRequest;
constructor(private fb:FormBuilder,private _service: SupplierService,private _commonService:CommonService){
this.supplierRequest= new SupplierRequest();
}
ngOnInit():void{
  this.SetValidation();
  this.getAllSupplier()
}
ngAfterViewInit() {
  $('#supplierModal').on('shown.bs.modal', () => {
    this.nameInputfield.nativeElement.focus();
  });
}

  SetValidation(){
    this.supplierFormGroup=this.fb.group({
      name:['',Validators.required],
      contactPerson:['',Validators.required],
      phone:['',Validators.required],
      email:['',[Validators.required,Validators.email]],
      address:['',Validators.required]
    })
  }
  saveDetails(){
    debugger
if(this.supplierFormGroup.valid){
  this._service.AddSupplier(this.supplierFormGroup.value).subscribe((response)=>{
     this._commonService.showToast(response.message,'','success') 
     this.getAllSupplier();
     $('#supplierModal').modal('hide');
  },
    (error)=>{
     
        this._commonService.showToast(" Error Try Again", '', 'error')     
    })
   }
this.supplierFormGroup.markAllAsTouched();
  }
  deleteDetails(){
    this._service.DeleteSupplier(this.supplierId).subscribe((response)=>{
      this._commonService.showToast(response.message,'','success') 
      this.getAllSupplier();
      $('#deleteModal').modal('hide');
   },
     (error)=>{
      
         this._commonService.showToast(" Error Try Again", '', 'error')     
     })
    }
  
  updateDetails(){
this.supplierRequest= this.supplierFormGroup.value;
this.supplierRequest.supplierId=this.supplierId;
    if(this.supplierFormGroup.valid){
      this._service.UpdateSupplier(this.supplierRequest).subscribe((response)=>{
         this._commonService.showToast(response.message,'','success') 
         this.getAllSupplier();
         $('#supplierModal').modal('hide');
      },
        (error)=>{
         
            this._commonService.showToast(" Error Try Again", '', 'error')     
        })
       }
    this.supplierFormGroup.markAllAsTouched();
      
  }
  getAllSupplier(){
this._service.GetAllSupplier().subscribe((response)=>{
  this.SupplierResponse=response.data;
  this.filteredSupplierList=response.data;
})
  }
  opensupplierModal() {
    this.supplierFormGroup.reset();
    this.isModelCheck = true;
    $('#supplierModal').modal('show');
  }
  editsupplier(response:any ){
    this.isModelCheck = false;
    $('#supplierModal').modal('show');
    this.supplierFormGroup.patchValue(response);
    this.supplierId=response.supplierId;
  }
  filterSuppliers() {
    if (!this.searchText || this.searchText.trim() === '') {
      this.filteredSupplierList = [...this.SupplierResponse];
    } else {
      this.filteredSupplierList = this.SupplierResponse.filter(supplier =>
        supplier.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
        supplier.email.toLowerCase().includes(this.searchText.toLowerCase())
      );
    }
    this.currentPage = 1; 
  }

  openDeleteModal(response:any){
    $('#deleteModal').modal('show');
    this.supplierId=response.supplierId;
    this.deleteText=response.name;
  }
}
