이번엔 상태머신을 이용해서 플레이어의 움직임을 구현해 봤습니다.

먼저 상태머신이라는 것을 알아보자면

> ### 상태머신
유한 상태머신은 컴퓨터 프로그램과 전자논리회로를 설계하는데 쓰이는 수학적 모델이다.
유한한 개수의 상태를 가질수 있는 오토마타 (즉, 추상기계라고 할 수 있다.)
한번에 오로지 한개의 상태를 가지게 되며,
현재상태란 -> 임의의 주어진 시간의 상태
어떤 Event에 의해 상태가 변할수 있으며, 이를 천이(Transition) 라고 부른다.
특정한 유한 오토마톤은 현재 상태로부터 가능한 천이 상태와 이런 천이를 유발하는 조건들의 집합으로 정의된다.


상태머신이란 위에서 설명한 것과 같이 한개의 상태를 가지고 있다가
event를 통해 상태를 바꿔가면서 원하는 행위를 할 수 있는 디자인 패턴인 듯 하다.

상태머신을 사용하면 각 상태와 각 행동을 따로 만들어 사용하고 기본적인 뼈대에 살을 붙이는 형태로 제작되기 때문에 굉장히 안정적으로 확장을 할 수 있는 듯하다.

하지만 계속 사용해 보면서도 이해하기 어렵고 사용하기 어려운 느낌을 아직 지울 순 없다....


 
-------------------------------------------
그래서 개인적으로 정리하면서 공부 구현하기로 했다.

![](https://velog.velcdn.com/images/shg6862/post/14761477-42aa-47d9-849c-dd8407f092d8/image.png)

![](https://velog.velcdn.com/images/shg6862/post/db42c57c-c602-4bed-9229-c6280a7fdbc4/image.png)

![](https://velog.velcdn.com/images/shg6862/post/1d2a4f11-1950-4342-9427-82b364b7be28/image.png)

먼저 IState라는 인터페이스를 BaseState가 받고
BaseState에서 크게 Ground, Air, Attack 상태가 받으면
Ground 내부에 Idle Walk Run 등이 붙는것처럼
다른 필요한 행위들이 있다면 앞선 상태들처럼 상속받아 이어붙이면 될 것이다.

그리고 StateMachine이라는 상태 천이와 천이됬을때 발생할 이벤트를 추상 클래스로 만들고
Player와 Enemy 등이 각각에 맡는 PlayerStateMachine 이나 EnemyStateMachine 등으로 받아서 사용할 수 있도록 한다.

그럼 Player 스크립트에서 초기 상태의 SO와 PlayerStateMachine을 받아 행동 상태를 변화시키면서 직접 움직 일 수 있다


------------------------------------------------------------------------------------------------------------
제출 이후 현재 상태
 - 달리기하다 180도 꺾어서 뛰면 Ground 상태부터 다시시작 되는 듯함
    (아마도 PlayerIdleState의 Update 부분에서 MovementInput이 Vector2.zero가 아니면 이부분에서 정확히 반대로 꺾으면 벡터값이 zero가 되는 듯하다
     하지만 MovementInput을 안받으면 초기 입력 자체를 인식하지 못하는데.....어쩌지......
     시도 1 : MoveSpeedModifire가 0이 아닐때 => input이 안들어오면 말짱꽝)
     시도 2 : MovementInput값이 초기 한번 들어 온 이후엔 저장되어 있는 듯 하니 null이 아니면 으로 작동 =>
 
 -  OnRunCanceled가 원하는데로 작동하지 않는 버그
    (달리는 중에 shift에서 손을떼도 w,a,s,d가 눌리고있다면 현상태 유지하며 달림
     아마 stateMachine.isRuned가 true값이 유지되는 듯함 ...... 분명 OnRunCanceled에서 false값으로 바꾸게 했는데
     이러면 애초에 OnRuncanceled가 불리지 않는다는 판정일테니... 구독된게 사라졌다 라고 생각해야하나?)
 
 -  보스 몹 구현할 예정
    (일단 Idle Run Jump 등은 기본 플레이어를 기초로 참고해서 작동시킬 예정
     RangedAttackState를 만들어서 일정 거리 이상은 원거리공격,
     그 이하는 점프공격 상태로 변환시킬 예쩡)
   
