import { AbstractControl, FormControl } from '@angular/forms';

export function ValidateImageType(type: string[]) {
    return function (control: FormControl) {
        const file = control.value;
        if ( file ) {
          const extension = file.name.split('.')[1].toLowerCase();
          let ind = false;
          for(let i=0;i<type.length;i++){
            if(extension==type[i].toLowerCase()){
             return null;   
            }
          }
          return {
            requiredFileType: true
          };
          
        }
    
        return null;
      };
}