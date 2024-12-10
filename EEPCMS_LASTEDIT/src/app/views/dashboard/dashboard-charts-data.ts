import { Injectable } from '@angular/core';
import { ChartData, ChartDataset, ChartOptions, ChartType } from 'chart.js';
import { Patient } from 'src/Models/patient.model';

export interface IChartProps {
  data?: ChartData;
  options?: ChartOptions;
  type: ChartType;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardChartsData {
  public mainChart: IChartProps = { type: 'line' };

  initMainChart(period: string, patients: Patient[]): void {
    const labels = period === 'Month'
      ? this.generateMonthlyLabels()
      : (period === 'Year' ? this.generateYearlyLabels() : this.generateWeeklyLabels());

    const datasets: ChartDataset[] = [{
      data: this.generateData(patients, period),
      label: 'Patient Count',
      borderColor: '#20a8d8',
      fill: period === 'Year' ? true : false 
    }];

    this.mainChart.data = { labels, datasets };
    this.mainChart.options = {
      responsive: true,
      scales: {
           y: {
        beginAtZero: true,
        ticks: {
          stepSize: 1, 
          callback: (value) => Number.isInteger(value) ? value : null 
        }
      }
      }
    };
  }

  private generateData(patients: Patient[], period: string): number[] {
    if (period === 'Month') {
      const monthlyCounts = Array(12).fill(0);
      patients.forEach(patient => {
        const month = new Date(patient.assigneddate).getMonth();
        monthlyCounts[month]++;
      });
      return monthlyCounts;
    }

    if (period === 'Year') {
      const yearlyCounts = Array(12).fill(0);
      patients.forEach(patient => {
        const month = new Date(patient.assigneddate).getMonth();
        yearlyCounts[month]++;
      });
      return yearlyCounts; 
    }

    if (period === 'Week') {
      const weeklyCounts = Array(7).fill(0);
      patients.forEach(patient => {
        const assignedDate = new Date(patient.assigneddate);
        const day = assignedDate.getDay();
        weeklyCounts[day]++;
      });
      return weeklyCounts;
    }

    return [];
  }

  private generateMonthlyLabels(): string[] {
    return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  }

  private generateYearlyLabels(): string[] {
    const currentYear = new Date().getFullYear();
    return Array.from({ length: 12 }, (_, i) => new Date(currentYear, i).toLocaleString('default', { month: 'long' }));
  }

  private generateWeeklyLabels(): string[] {
    return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  }
}
