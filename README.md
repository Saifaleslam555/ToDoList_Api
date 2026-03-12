# ToDoList API - Advanced .NET Playground

<div align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet" alt=".NET">
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-blue" alt="ASP.NET Core Web API">
  <img src="https://img.shields.io/badge/EF_Core-ORM-green" alt="Entity Framework Core">
</div>

## 📌 عن المشروع (About The Project)

مرحباً بك في هذا المستودع! 
**هذا ليس مجرد تطبيق ToDoList تقليدي.** بل هو **حقل تجارب شخصي (Playground)** ومختبر برمجي أستخدمه لتطبيق، اختبار، وتوثيق مفاهيم `.NET` المتقدمة وأفضل الممارسات (Best Practices) في بناء تطبيقات الـ Web API قابلة للتوسع والصيانة.

اخترت فكرة الـ ToDoList لأن بساطة فكرة المشروع (Business Logic) تسمح لي بالتركيز بشكل كامل على **البنية المعمارية (Architecture)** و**أنماط التصميم (Design Patterns)** بدلاً من تشتيت الانتباه في تعقيدات الفكرة نفسها.

---

## 🛠️ البنية المعمارية وأنماط التصميم (Architecture & Design Patterns)

تم بناء هذا المشروع مع التركيز الشديد على **فصل الاهتمامات (Separation of Concerns)** وكتابة كود نظيف (Clean Code). من أبرز الأنماط المطبقة:

* **Layered Design:** تقسيم المشروع إلى طبقات منطقية (Controllers, Services, Repositories).
* **Repository Pattern:** لفصل منطق الوصول إلى قواعد البيانات (Data Access) عن باقي أجزاء التطبيق.
* **Service Layer Pattern:** لتغليف منطق الأعمال (Business Logic) بعيداً عن الـ Controllers.
* **DTOs (Data Transfer Objects):** لنقل البيانات بأمان بين العميل والخادم دون تعريض نماذج قاعدة البيانات (Domain Models) للخطر.

---

## 💻 التقنيات والمكتبات المستخدمة (Tech Stack & Libraries)

* **ASP.NET Core Web API:** إطار العمل الأساسي.
* **Entity Framework Core (EF Core):** للتعامل مع قاعدة البيانات (ORM) باستخدام الـ Code-First Approach و الـ Migrations.
* **AutoMapper:** لتبسيط عملية تحويل الكائنات (Mapping) بين الـ Models والـ DTOs.
* **FluentValidation** *(أو أي مكتبة تحقق مستخدمة)*: للتحقق من صحة البيانات المدخلة (Input Validation) بشكل نظيف.
* **JWT Authentication:** لإدارة عمليات تسجيل الدخول والتوثيق (كما هو موضح في `AuthService`).

---

## 🧪 مفاهيم يتم اختبارها وتطبيقها (Concepts in Action)

* **Dependency Injection (DI):** إدارة التبعيات بشكل احترافي، واستخدام الـ Extension Methods (في مجلد `Extensions`) لتنظيف ملف `Program.cs`.
* **Custom Middlewares:** معالجة الأخطاء المركزية (Global Error Handling) والتحكم في مسار الطلبات (Pipeline).
* **Asynchronous Programming:** استخدام `async/await` في جميع العمليات التي تتطلب I/O لتحسين الأداء.

---

## 🚀 كيفية التشغيل (How to Run)

1. قم بعمل Clone للمستودع:
   ```bash
   git clone [https://github.com/Saifaleslam555/ToDoList_Api.git](https://github.com/Saifaleslam555/ToDoList_Api.git)
