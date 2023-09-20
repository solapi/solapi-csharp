﻿using System;
using System.Collections.Generic;

static class SendAlimtalk
{
    public static void Run()
    {
        // 한번 요청으로 1만건의 알림톡 발송이 가능합니다.
        // 등록되어 있는 템플릿의 변수 부분을 제외한 나머지 부분(상수)은 100% 일치해야 합니다.
        // 템플릿 내용이 "#{이름}님 가입을 환영합니다."으로 등록되어 있는 경우 변수 #{이름}을 홍길동으로 치환하여 "홍길동님 가입을 환영합니다."로 입력해 주세요.
        // 버튼은 5개까지 입력 가능합니다.

        MessagingLib.Messages messages = new MessagingLib.Messages();

        // #{변수} 에 값을 입력합니다. (템플릿의 모든 변수값을 입력해야 오류 발생하지 않습니다.)
        Dictionary<string, string> variables = new Dictionary<string, string> { { "#{key1}", "value1" }, { "#{key2}", "value2" } };

        // variables에 변수값을 입력하여 요청하면 알림톡의 내용은 서버쪽에서 자동으로 채워져 발송됩니다.
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000001",
            from = "029302266",
            kakaoOptions = new MessagingLib.KakaoOptions()
            {
                pfId = "KA01PF190626020502205cl0mYSoplA2",
                templateId = "KA01TP190626032036196g86q1RGN7D1", // 템플릿 등록 후 발급받은 값을 사용해 주세요
                variables = variables
            }
        });

        // variables 사용하지 않고 수동 발송 에시 (모든 종류의 버튼 예시) - 실 사용으로 권하지 않습니다. (참고용)
        messages.Add(new MessagingLib.Message()
        {
            to = "01000000003",
            from = "029302266",
            text = "안녕하세요." + Environment.NewLine + "홍길동님 회원가입을 환영합니다." + Environment.NewLine + "아래 다양한 형식의 버튼을 통해 사용방법을 익히실 수 있습니다.",
            kakaoOptions = new MessagingLib.KakaoOptions()
            {
                pfId = "KA01PF190626020502205cl0mYSoplA2",
                templateId = "KA01TP190626032036196g86q1RGN7D3",
                buttons = new MessagingLib.KakaoButton[]
                {
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "WL",
                        buttonName = "시작하기",
                        linkMo = "https://m.example.com",
                        linkPc = "https://example.com"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "AL",
                        buttonName = "앱 실행",
                        linkAnd = "examplescheme://",
                        linkIos = "examplescheme://"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "DS",
                        buttonName = "배송조회"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "BK",
                        buttonName = "봇키워드"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "MD",
                        buttonName = "상담요청하기"
                    },
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "AC",
                        buttonName = "채널추가"
                    },
                    // 상담톡 서비스 사용 시 가능
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "BC",
                        buttonName = "상담톡 전환"
                    },
                    // 챗봇 사용 시 가능
                    new MessagingLib.KakaoButton()
                    {
                        buttonType = "BT",
                        buttonName = "챗봇 문의"
                    }
                }
            }
        });

        // 1만건까지 추가 가능

        MessagingLib.Response response = MessagingLib.SendMessages(messages);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("전송 결과");
            Console.WriteLine("Group ID:" + response.Data.SelectToken("groupId").ToString());
            Console.WriteLine("Status:" + response.Data.SelectToken("status").ToString());
            Console.WriteLine("Count:" + response.Data.SelectToken("count").ToString());
        }
        else
        {
            Console.WriteLine("Error Code:" + response.ErrorCode);
            Console.WriteLine("Error Message:" + response.ErrorMessage);
        }
    }
}
