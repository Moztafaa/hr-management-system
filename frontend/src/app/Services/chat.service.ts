import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { ChatMessage, ChatRequest, ChatResponse } from '../models/chat';
import { ApiResponse } from '../models/api-response';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7005/api/Chat';

  // Reactive state
  private _messages = signal<ChatMessage[]>([]);
  private _isLoading = signal<boolean>(false);

  // Public readonly signals
  messages = this._messages.asReadonly();
  isLoading = this._isLoading.asReadonly();

  // Generate unique ID for messages
  private generateId(): string {
    return `msg-${Date.now()}-${Math.random().toString(36).substring(2, 9)}`;
  }

  // Add user message to chat
  addUserMessage(content: string): void {
    const message: ChatMessage = {
      id: this.generateId(),
      content,
      isUser: true,
      timestamp: new Date(),
    };
    this._messages.update((messages) => [...messages, message]);
  }

  // Add AI response to chat
  private addAIMessage(content: string): void {
    const message: ChatMessage = {
      id: this.generateId(),
      content,
      isUser: false,
      timestamp: new Date(),
    };
    this._messages.update((messages) => [...messages, message]);
  }

  // Send question to AI
  askQuestion(question: string): Observable<ApiResponse<ChatResponse>> {
    this._isLoading.set(true);
    this.addUserMessage(question);

    const request: ChatRequest = { question };

    return this.http.post<ApiResponse<ChatResponse>>(this.apiUrl, request).pipe(
      tap({
        next: (response) => {
          if (!response.hasError && response.data) {
            this.addAIMessage(response.data.answer);
          } else {
            this.addAIMessage(
              response.errorMessage || 'Sorry, I encountered an error. Please try again.'
            );
          }
          this._isLoading.set(false);
        },
        error: (error) => {
          console.error('Chat error:', error);
          this.addAIMessage(
            'Sorry, I encountered an error connecting to the server. Please try again later.'
          );
          this._isLoading.set(false);
        },
      })
    );
  }

  // Clear chat history
  clearMessages(): void {
    this._messages.set([]);
  }

  // Get initial greeting
  getGreeting(): ChatMessage {
    return {
      id: this.generateId(),
      content:
        "Hello! I'm your HR Assistant. I can help you with questions about employees, departments, leave policies, and other HR-related topics. How can I assist you today?",
      isUser: false,
      timestamp: new Date(),
    };
  }

  // Initialize chat with greeting
  initializeChat(): void {
    if (this._messages().length === 0) {
      this._messages.set([this.getGreeting()]);
    }
  }
}
