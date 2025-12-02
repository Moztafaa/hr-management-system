export interface ChatMessage {
  id: string;
  content: string;
  isUser: boolean;
  timestamp: Date;
}

export interface ChatRequest {
  question: string;
}

export interface ChatResponse {
  answer: string;
  timestamp: string;
}
