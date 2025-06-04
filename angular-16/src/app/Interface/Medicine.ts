export class MedicineResponse {
    Name: string | null;
    categoryId: number;
    supplierId: number;
  
    constructor() {
      this.Name = '';       // Default to null
      this.categoryId = 0;    // Default to 0
      this.supplierId = 0;    // Default to 0
    }
  }