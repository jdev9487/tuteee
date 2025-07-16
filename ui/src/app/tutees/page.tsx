import Link from "next/link"
import { tuteeResponse } from "../types/TuteeResponse"

export default async function Page() {
  const data = await fetch('http://localhost:5078/tutees')
  const tutees = (await data.json()) as tuteeResponse[]
  return (
    <div>
      <h1>Tutees</h1>
      {tutees.map((t, i) => <Link key={i} href={`/tutees/${t.tuteeId}`}><p>{`${t.firstName} ${t.lastName}`}</p></Link>)}
    </div>
  )
}