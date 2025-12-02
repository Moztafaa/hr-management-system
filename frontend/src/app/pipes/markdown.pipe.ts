import { Pipe, PipeTransform } from '@angular/core';
import { marked } from 'marked';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Pipe({
  name: 'markdown',
  standalone: true,
})
export class MarkdownPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {
    // Configure marked options
    marked.setOptions({
      breaks: true, // Convert \n to <br>
      gfm: true, // GitHub Flavored Markdown
    });
  }

  transform(value: string): SafeHtml {
    if (!value) {
      return '';
    }

    const html = marked.parse(value) as string;
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
}
