import { tuteeResponse } from "./TuteeResponse"

export type guardianResponse = {
    guardianId: number, 
    firstName: string,
    lastName: string,
    tutees: tuteeResponse[]
}

