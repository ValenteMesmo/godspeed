module Events

type State = Requested | Processing | Done  
let mutable Save = Done
