import { tuteeResponse } from "../../types/TuteeResponse"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`http://localhost:5078/tutees/${slug}`)
  const tutee = (await data.json()) as tuteeResponse
  return (
    <div>
      <h1>{tutee.firstName} {tutee.lastName}</h1>
      <h2>Lessons</h2>
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Start</th>
            <th>End</th>
          </tr>
        </thead>
        <tbody>
          {tutee.lessons.map((l, i) => 
            <tr key={i}>
              <td>date</td>
              <td>{l.startTime}</td>
              <td>{l.endTime}</td>
            </tr>
          )}
        </tbody>
      </table> 
    </div>
  )
}