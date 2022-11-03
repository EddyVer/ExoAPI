export interface User {
  id: number;
  name: string;
  password: string;
  grade: Grades;
}

export enum Grades {
  admin = 0,
  user = 1,
}
