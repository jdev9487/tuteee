import { tuteeResponse } from "./TuteeResponse"

export type accountResponse = {
    accountId: number, 
    holderFirstName: string,
    holderLastName: string,
    tutees: tuteeResponse[]
}

