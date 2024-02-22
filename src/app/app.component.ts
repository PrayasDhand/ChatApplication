import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'ChatApplication';

  constructor() {}

  ngOnInit(): void {
    window.onload = () => {
      const header = document.getElementById('myHeader');
      const page = document.getElementById('page');
      const openMenuButton = document.getElementById('openmenu');
  
      window.addEventListener('scroll', () => {
        page?.classList.remove('menuopen');
        if (window.scrollY >= 100) {
          header?.classList.add('sticky');
        } else {
          header?.classList.remove('sticky');
        }
      });
  
      openMenuButton?.addEventListener('click', () => {
        header?.classList.remove('sticky');
        page?.classList.add('menuopen');
      });
  
      const links = document.querySelectorAll('a[href^="#"]');
  
      links.forEach((link: Element) => {
        const linkElement = link as HTMLElement;
        linkElement.addEventListener('click', (event) => {
          event.preventDefault();
          const targetId = linkElement.getAttribute('href');
          const targetElement = document.querySelector(targetId!);
          if (targetElement) {
            targetElement.scrollIntoView({ behavior: 'smooth' });
          }
        });
      });
    };
  }
  
}
