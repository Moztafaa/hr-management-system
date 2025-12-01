import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
  standalone: true,
})
export class FilterPipe implements PipeTransform {
  transform(items: any[], property: string, value: any): any[] {
    if (!items || !property || value === undefined || value === null) {
      return items || [];
    }
    return items.filter((item) => item[property] === value);
  }
}
