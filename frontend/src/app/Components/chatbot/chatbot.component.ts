import { Component, inject, OnInit, ElementRef, ViewChild, AfterViewChecked } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../../Services/chat.service';
import { ChatMessage } from '../../models/chat';
import { MarkdownPipe } from '../../pipes/markdown.pipe';

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [CommonModule, FormsModule, MarkdownPipe],
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.css'],
})
export class ChatbotComponent implements OnInit, AfterViewChecked {
  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;
  @ViewChild('messageInput') private messageInput!: ElementRef;

  private chatService = inject(ChatService);

  isOpen = false;
  userMessage = '';
  messages = this.chatService.messages;
  isLoading = this.chatService.isLoading;

  private shouldScrollToBottom = false;

  ngOnInit(): void {
    this.chatService.initializeChat();
  }

  ngAfterViewChecked(): void {
    if (this.shouldScrollToBottom) {
      this.scrollToBottom();
      this.shouldScrollToBottom = false;
    }
  }

  toggleChat(): void {
    this.isOpen = !this.isOpen;
    if (this.isOpen) {
      this.shouldScrollToBottom = true;
      setTimeout(() => {
        this.messageInput?.nativeElement?.focus();
      }, 100);
    }
  }

  closeChat(): void {
    this.isOpen = false;
  }

  sendMessage(): void {
    const question = this.userMessage.trim();
    if (!question || this.isLoading()) {
      return;
    }

    this.userMessage = '';
    this.shouldScrollToBottom = true;

    this.chatService.askQuestion(question).subscribe({
      next: () => {
        this.shouldScrollToBottom = true;
      },
      error: () => {
        this.shouldScrollToBottom = true;
      },
    });
  }

  onKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  clearChat(): void {
    this.chatService.clearMessages();
    this.chatService.initializeChat();
  }

  private scrollToBottom(): void {
    if (this.messagesContainer) {
      const element = this.messagesContainer.nativeElement;
      element.scrollTop = element.scrollHeight;
    }
  }

  trackByMessageId(index: number, message: ChatMessage): string {
    return message.id;
  }

  formatTime(date: Date): string {
    return new Date(date).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  }
}
