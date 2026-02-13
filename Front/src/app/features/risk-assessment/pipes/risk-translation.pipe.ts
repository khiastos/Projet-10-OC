import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'riskTranslation',
  standalone: true
})
export class RiskTranslationPipe implements PipeTransform {

  transform(value: 'None' | 'Borderline' | 'InDanger' | 'EarlyOnset'): string {

    switch (value) {
      case 'None':
        return 'Aucun';
      case 'Borderline':
        return 'Limite';
      case 'InDanger':
        return 'En danger';
      case 'EarlyOnset':
        return 'Début précoce';
      default:
        return value;
    }
  }
}
