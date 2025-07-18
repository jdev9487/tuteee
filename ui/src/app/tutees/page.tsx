import Link from "next/link"
import { tuteeResponse } from "../types/TuteeResponse"

export default async function Page() {
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/tutees`, { cache: 'no-store' })
  const tutees = (await data.json()) as tuteeResponse[]
  return (
    <div>
      <h1>Tutees</h1>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email address</th>
          </tr>
        </thead>
        <tbody>
          {tutees.map((t, i) => 
            <tr key={i}>
              <td><Link href={`/tutees/${t.tuteeId}`}><p>{`${t.firstName} ${t.lastName}`}</p></Link></td>
              <td>{t.emailAddress}</td>
            </tr>
          )}
        </tbody>
      </table> 
    </div>
  )
}