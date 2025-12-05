// Performance Module Enums

export enum CycleFrequency {
  Annual = 1,
  Quarterly = 2,
  Monthly = 3,
}

export enum RatingScaleType {
  OneToFive = 1,
  Percentile = 2,
}

export enum ReviewStatus {
  Draft = 0,
  InProgress = 1,
  Submitted = 2,
  Closed = 3,
}

export enum GoalStatus {
  NotStarted = 0,
  OnTrack = 1,
  AtRisk = 2,
  Completed = 3,
}

export enum FeedbackType {
  Self = 1,
  Manager = 2,
  Peer = 3,
}

// Helper functions for display
export function getCycleFrequencyLabel(frequency: CycleFrequency): string {
  switch (frequency) {
    case CycleFrequency.Annual:
      return 'Annual';
    case CycleFrequency.Quarterly:
      return 'Quarterly';
    case CycleFrequency.Monthly:
      return 'Monthly';
    default:
      return 'Unknown';
  }
}

export function getRatingScaleLabel(scale: RatingScaleType): string {
  switch (scale) {
    case RatingScaleType.OneToFive:
      return '1-5 Scale';
    case RatingScaleType.Percentile:
      return 'Percentile';
    default:
      return 'Unknown';
  }
}

export function getReviewStatusLabel(status: ReviewStatus): string {
  switch (status) {
    case ReviewStatus.Draft:
      return 'Draft';
    case ReviewStatus.InProgress:
      return 'In Progress';
    case ReviewStatus.Submitted:
      return 'Submitted';
    case ReviewStatus.Closed:
      return 'Closed';
    default:
      return 'Unknown';
  }
}

export function getReviewStatusClass(status: ReviewStatus): string {
  switch (status) {
    case ReviewStatus.Draft:
      return 'bg-secondary';
    case ReviewStatus.InProgress:
      return 'bg-info';
    case ReviewStatus.Submitted:
      return 'bg-warning';
    case ReviewStatus.Closed:
      return 'bg-success';
    default:
      return 'bg-secondary';
  }
}

export function getGoalStatusLabel(status: GoalStatus): string {
  switch (status) {
    case GoalStatus.NotStarted:
      return 'Not Started';
    case GoalStatus.OnTrack:
      return 'On Track';
    case GoalStatus.AtRisk:
      return 'At Risk';
    case GoalStatus.Completed:
      return 'Completed';
    default:
      return 'Unknown';
  }
}

export function getGoalStatusClass(status: GoalStatus): string {
  switch (status) {
    case GoalStatus.NotStarted:
      return 'bg-secondary';
    case GoalStatus.OnTrack:
      return 'bg-success';
    case GoalStatus.AtRisk:
      return 'bg-warning';
    case GoalStatus.Completed:
      return 'bg-primary';
    default:
      return 'bg-secondary';
  }
}

export function getFeedbackTypeLabel(type: FeedbackType): string {
  switch (type) {
    case FeedbackType.Self:
      return 'Self Assessment';
    case FeedbackType.Manager:
      return 'Manager Review';
    case FeedbackType.Peer:
      return 'Peer Feedback';
    default:
      return 'Unknown';
  }
}

export function getFeedbackTypeClass(type: FeedbackType): string {
  switch (type) {
    case FeedbackType.Self:
      return 'bg-info';
    case FeedbackType.Manager:
      return 'bg-primary';
    case FeedbackType.Peer:
      return 'bg-success';
    default:
      return 'bg-secondary';
  }
}
