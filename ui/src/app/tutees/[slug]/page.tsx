import { tuteeResponse } from "../../types/TuteeResponse"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`http://localhost:5078/tutees/${slug}`)
  const tutee = (await data.json()) as tuteeResponse

  function getDate(dateStr: string) {
    const date = new Date(dateStr)
    const formattedMonth = `${date.getMonth()}`.padStart(2, '0')
    const formattedDay = `${date.getDate()}`.padStart(2, '0')
    return `${formattedDay}/${formattedMonth}/${date.getFullYear()}`
  }

  function getTime(timeStr: string) {
    const date = new Date(timeStr)
    const formattedHours = `${date.getHours()}`.padStart(2, '0')
    const formattedMinutes = `${date.getMinutes()}`.padStart(2, '0')
    return `${formattedHours}:${formattedMinutes}`
  }

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
              <td>{getDate(l.startTime)}</td>
              <td>{getTime(l.startTime)}</td>
              <td>{getTime(l.endTime)}</td>
            </tr>
          )}
        </tbody>
      </table> 
    </div>
  )
}