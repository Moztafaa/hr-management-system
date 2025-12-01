export interface ApiResponse<T> {
  hasError: boolean;
  errorMessage: string;
  data: T | null;
}
