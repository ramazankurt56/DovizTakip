import { AfterViewInit, Component, OnDestroy } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { SignalrService } from '../../services/signalr.service';
import { CurrencyModel } from '../../models/currency.model';
import { DatePipe } from '@angular/common';
declare const Chart:any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SharedModule],
  providers: [DatePipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements AfterViewInit, OnDestroy {
  usdCurrencies: CurrencyModel[] = [];
  euroCurrencies: CurrencyModel[] = [];
  chart: any;
  private intervalId: any;

  constructor(
    private signalR: SignalrService,
    private date: DatePipe
  ) {
    this.signalR.startConnection(() => {
      this.signalR.hub?.on("Currency", (res: CurrencyModel) => {
        if (res.type.value === 1) {
          this.usdCurrencies.push(res);
        } else {
          this.euroCurrencies.push(res);
        }

        this.updateChart();
      });
    });
  }

  ngAfterViewInit(): void {
    this.showChart();
    this.startResetInterval();
  }

  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  showChart() {
    const ctx = document.getElementById('myChart');

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: [],
        datasets: [{
          label: '# USD hareketleri',
          data: [],
          borderWidth: 1
        },
        {
          label: '# Euro hareketleri',
          data: [],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }

  updateChart() {
    const labels = this.usdCurrencies.map((val) => this.date.transform(val.createdAt, "HH:mm:ss") ?? "00:00:00");
    this.chart.data.labels = labels;

    const usdData = this.usdCurrencies.map((val) => val.amount);
    const euroData = this.euroCurrencies.map((val) => val.amount);
    this.chart.data.datasets[0].data = usdData;
    this.chart.data.datasets[1].data = euroData;

    this.chart.update();
  }

  startResetInterval() {
    this.intervalId = setInterval(() => {
      this.resetData();
    }, 300000); 
  }

  resetData() {
    this.usdCurrencies = [];
    this.euroCurrencies = [];
    this.chart.data.labels = [];
    this.chart.data.datasets.forEach((dataset: any) => {
      dataset.data = [];
    });
    this.chart.update();
  }
}
