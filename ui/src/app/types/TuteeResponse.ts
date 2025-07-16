import { lessonResponse } from "./LessonResponse"

export type tuteeResponse = {
    tuteeId: number, 
    firstName: string,
    lastName: string,
    emailAddress: string,
    phoneNumber: string,
    lessons: lessonResponse[]
}

